using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers
{


    class IntcodeProcessor
    {
        private enum ParamMode
        {
            Position = 0,
            Immediate = 1,
            Relative = 2
        }

        private List<long> _initialProgram;
        private List<long> program;
        private int pointer = 0;

        private int relativeBase = 0;

        private readonly Queue<long> inputs = new();

        public long Output { get; set; } = 0;
        public long ValueAtZero => program[0];

        #region Constructors

        public IntcodeProcessor(string program)
        {
            this.program = program.Split(",").Select(x => long.Parse(x)).ToList();
            Enumerable.Range(0, 10000).ToList().ForEach(x => this.program.Add(0));

            _initialProgram = this.program.ToList();
        }

        public IntcodeProcessor(string program, params long[] inputs) : this(program)
        {
            AddInput(inputs);
        }

        #endregion

        public void ResetProgram()
        {
            program = _initialProgram.ToList();
        }

        #region Run Methods

        public void RunProgram(int noun, int verb)
        {
            program[1] = noun;
            program[2] = verb;

            RunProgram();
        }

        public void RunProgram()
        {
            pointer = 0;
            while (true)
            {
                long instruction = program[pointer] % 100;

                if (instruction == 99)
                {
                    return;
                }

                long paramModes = program[pointer] / 100;

                RunInstruction(instruction, paramModes);
            }
        }

        public bool RunTilOutput()
        {
            while (true)
            {
                long instruction = program[pointer] % 100;

                if (instruction == 99)
                {
                    return true;
                }

                long paramModes = program[pointer] / 100;

                RunInstruction(instruction, paramModes);

                if (instruction == 4)
                {
                    return false;
                }
            }
        }

        private void RunInstruction(long instruction, long paramModes)
        {
            switch (instruction)
            {
                case 1:
                    RunSum(paramModes);
                    pointer += 4;
                    break;
                case 2:
                    RunMulti(paramModes);
                    pointer += 4;
                    break;
                case 3:
                    RunInput(paramModes);
                    pointer += 2;
                    break;
                case 4:
                    RunOutput(paramModes);
                    pointer += 2;
                    break;
                case 5:
                    RunJump(paramModes, true);
                    break;
                case 6:
                    RunJump(paramModes, false);
                    break;
                case 7:
                    RunLessThan(paramModes);
                    pointer += 4;
                    break;
                case 8:
                    RunEquals(paramModes);
                    pointer += 4;
                    break;
                case 9:
                    RunAdjustBase(paramModes);
                    pointer += 2;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Managing Input

        public void AddInput(params long[] inputs)
        {
            foreach (var input in inputs)
            {
                this.inputs.Enqueue(input);
            }
        }

        #endregion

        #region Instructions

        private void RunSum(long modes)
        {
            var parameters = GetParams(3, modes);
            var param1 = program[parameters[0]];
            var param2 = program[parameters[1]];

            program[parameters[2]] = param1 + param2;
        }

        private void RunMulti(long modes)
        {
            var parameters = GetParams(3, modes);
            var param1 = program[parameters[0]];
            var param2 = program[parameters[1]];

            program[parameters[2]] = param1 * param2;
        }

        private void RunInput(long modes)
        {
            program[GetParam(pointer + 1, modes)] = inputs.Dequeue();
        }

        private void RunOutput(long modes)
        {
            Output = program[GetParam(pointer + 1, modes)];
        }

        private void RunJump(long modes, bool isTrue)
        {
            var parameters = GetParams(2, modes);
            var param1 = program[parameters[0]];
            var param2 = (int)program[parameters[1]];

            if (isTrue)
            {
                if (param1 != 0) pointer = param2;
                else pointer += 3;
            }
            else
            {
                if (param1 == 0) pointer = param2;
                else pointer += 3;
            }
        }

        private void RunLessThan(long modes)
        {
            var parameters = GetParams(3, modes);
            var param1 = program[parameters[0]];
            var param2 = program[parameters[1]];

            program[parameters[2]] = param1 < param2 ? 1 : 0;
        }

        private void RunEquals(long modes)
        {
            var parameters = GetParams(3, modes);
            var param1 = program[parameters[0]];
            var param2 = program[parameters[1]];

            program[parameters[2]] = param1 == param2 ? 1 : 0;
        }

        private void RunAdjustBase(long modes)
        {
            relativeBase += (int)program[GetParam(pointer + 1, modes % 10)];
        }

        #endregion

        #region Getting Parameters

        private List<int> GetParams(int length, long modes)
        {
            List<int> parameters = new();

            for (int i = 0; i < length; i++)
            {
                parameters.Add(GetParam(pointer + i + 1, modes % 10));
                modes /= 10;
            }

            return parameters;
        }

        private int GetParam(int location, long mode)
        {
            return GetParam(location, (ParamMode)mode);
        }

        private int GetParam(int location, ParamMode mode)
        {
            return mode switch
            {
                ParamMode.Position => (int)program[location],
                ParamMode.Immediate => location,
                ParamMode.Relative => (int)program[location] + relativeBase,
                _ => throw new NotImplementedException(),
            };
        }

        #endregion
    }
}
