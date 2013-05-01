//
// Encog(tm) Core v3.1 - .Net Version
// http://www.heatonresearch.com/encog/
//
// Copyright 2008-2012 Heaton Research, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//   
// For more information on Heaton Research copyrights, licenses 
// and trademarks visit:
// http://www.heatonresearch.com/copyright
//
using System;
using ConsoleExamples.Examples;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks.Training;
using Encog.Util.Simple;

namespace Encog.Examples.XOR
{
    public class XORNEAT : IExample
    {
        /// <summary>
        /// Input for the XOR function.
        /// </summary>
        public static double[][] XORInput = {
                                                new double[2] {0.0, 0.0},
                                                new double[2] {1.0, 0.0},
                                                new double[2] {0.0, 1.0},
                                                new double[2] {1.0, 1.0}
                                            };

        /// <summary>
        /// Ideal output for the XOR function.
        /// </summary>
        public static double[][] XORIdeal = {
                                                new double[1] {0.0},
                                                new double[1] {1.0},
                                                new double[1] {1.0},
                                                new double[1] {0.0}
                                            };

        public static ExampleInfo Info
        {
            get
            {
                var info = new ExampleInfo(
                    typeof (XORNEAT),
                    "xor-neat",
                    "Simple XOR with NEAT.",
                    "This example shows how to train an XOR using NEAT.");
                return info;
            }
        }

        #region IExample Members

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="app">Holds arguments and other info.</param>
        public void Execute(IExampleInterface app)
        {
            /*
            IMLDataSet trainingSet = new BasicMLDataSet(XORInput, XORIdeal);
            var pop = new NEATPopulation(2, 1, 1000);
            ICalculateScore score = new TrainingSetScore(trainingSet);
            // train the neural network
            var step = new ActivationStep();
            step.Center = 0.5;
            pop.OutputActivationFunction = step;

            var train = new NEATTraining(score, pop);

            EncogUtility.TrainToError(train, 0.01);

            var network = (NEATNetwork) train.Method;

            network.ClearContext();
            // test the neural network
            Console.WriteLine(@"Neural Network Results:");
            EncogUtility.Evaluate(network, trainingSet);*/
        }

        #endregion
    }
}
