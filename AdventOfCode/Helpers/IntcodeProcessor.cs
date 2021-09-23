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
            var param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            var param2 = GetParam(pointer + 2, modes % 10);
            modes /= 10;
            var param3 = GetOutParam(pointer + 3, modes % 10);

            program[param3] = param1 + param2;
        }

        private void RunMulti(long modes)
        {
            var param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            var param2 = GetParam(pointer + 2, modes % 10);
            modes /= 10;
            var param3 = GetOutParam(pointer + 3, modes % 10);

            program[param3] = param1 * param2;
        }

        private void RunInput(long modes)
        {
            program[GetOutParam(pointer + 1, modes % 10)] = inputs.Dequeue();
        }

        private void RunOutput(long modes)
        {
            Output = GetParam(pointer + 1, modes);
        }

        private void RunJump(long modes, bool isTrue)
        {
            var param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            var param2 = (int)GetParam(pointer + 2, modes % 10);

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
            var param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            var param2 = GetParam(pointer + 2, modes % 10);
            modes /= 10;
            var param3 = GetOutParam(pointer + 3, modes % 10);

            program[param3] = param1 < param2 ? 1 : 0;
        }

        private void RunEquals(long modes)
        {
            var param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            var param2 = GetParam(pointer + 2, modes % 10);
            modes /= 10;
            var param3 = GetOutParam(pointer + 3, modes % 10);

            program[param3] = param1 == param2 ? 1 : 0;
        }

        private void RunAdjustBase(long modes)
        {
            var param1 = GetParam(pointer + 1, modes % 10);
            relativeBase += (int)param1;
        }

        #endregion

        private long GetParam(int location, long mode)
        {
            return GetParam(location, (ParamMode)mode);
        }

        // TODO: Figure this the fuck out
        private int GetOutParam(int location, long mode)
        {
            if (mode == 2)
            {

                return (int)program[location] + relativeBase;
            }
            else
            {
                return (int)program[location];
            }
        }
        private long GetParam(int location, ParamMode mode)
        {
            return mode switch
            {
                ParamMode.Position => program[(int)program[location]],
                ParamMode.Immediate => program[location],
                ParamMode.Relative => program[(int)program[location] + relativeBase],
                _ => throw new NotImplementedException(),
            };
        }
    }
}
