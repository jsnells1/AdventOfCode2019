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

        private int[] _initialProgram;
        private List<int> program;
        private int pointer = 0;

        private int relativeBase = 0;

        private readonly Queue<int> inputs = new();

        public int Output { get; set; } = 0;
        public int ValueAtZero => program[0];

        #region Constructors

        public IntcodeProcessor(int[] program)
        {
            // Copy intiial array for reuse 
            _initialProgram = program.ToArray();
            this.program = program.ToList();

            Enumerable.Range(0, 1000).ToList().ForEach(x => this.program.Add(0));
        }

        public IntcodeProcessor(int[] program, params int[] inputs) : this(program)
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
                int instruction = program[pointer] % 100;

                if (instruction == 99)
                {
                    return;
                }

                int paramModes = program[pointer] / 100;

                RunInstruction(instruction, paramModes);
            }
        }

        public bool RunTilOutput()
        {
            while (true)
            {
                int instruction = program[pointer] % 100;

                if (instruction == 99)
                {
                    return true;
                }

                int paramModes = program[pointer] / 100;

                RunInstruction(instruction, paramModes);

                if (instruction == 4)
                {
                    return false;
                }
            }
        }

        private void RunInstruction(int instruction, int paramModes)
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

        public void AddInput(params int[] inputs)
        {
            foreach (var input in inputs)
            {
                this.inputs.Enqueue(input);
            }
        }

        #endregion

        #region Instructions

        private void RunSum(int modes)
        {
            int param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            int param2 = GetParam(pointer + 2, modes % 10);
            int param3 = program[pointer + 3];

            program[param3] = param1 + param2;
        }

        private void RunMulti(int modes)
        {
            int param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            int param2 = GetParam(pointer + 2, modes % 10);
            int param3 = program[pointer + 3];

            program[param3] = param1 * param2;
        }

        private void RunInput(int modes)
        {
            program[program[pointer + 1]] = inputs.Dequeue();
        }

        private void RunOutput(int modes)
        {
            //Output = program[program[pointer + 1]];
            Output = GetParam(pointer + 1, modes);
        }

        private void RunJump(int modes, bool isTrue)
        {
            int param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            int param2 = GetParam(pointer + 2, modes % 10);

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

        private void RunLessThan(int modes)
        {
            int param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            int param2 = GetParam(pointer + 2, modes % 10);
            int param3 = program[pointer + 3];

            program[param3] = param1 < param2 ? 1 : 0;
        }

        private void RunEquals(int modes)
        {
            int param1 = GetParam(pointer + 1, modes % 10);
            modes /= 10;
            int param2 = GetParam(pointer + 2, modes % 10);
            int param3 = program[pointer + 3];

            program[param3] = param1 == param2 ? 1 : 0;
        }

        private void RunAdjustBase(int modes)
        {
            int param1 = GetParam(pointer + 1, modes % 10);
            relativeBase += param1;
        }

        #endregion

        private int GetParam(int location, int mode)
        {
            return GetParam(location, (ParamMode)mode);
        }

        private int GetParam(int location, ParamMode mode)
        {
            switch (mode)
            {
                case ParamMode.Position:
                    return program[program[location]];
                case ParamMode.Immediate:
                    return program[location];
                case ParamMode.Relative:
                    return program[program[location] + relativeBase];

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
