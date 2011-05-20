using System;
using Encog.MathUtil;
using Encog.ML;
using Encog.ML.Data;
using Encog.ML.Train;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation;
using Encog.Util;

namespace Encog.Neural.CPN.Training
{
    /// <summary>
    /// Used for Instar training of a CPN neural network. A CPN network is a hybrid
    /// supervised/unsupervised network. The Instar training handles the unsupervised
    /// portion of the training.
    /// </summary>
    ///
    public class TrainInstar : BasicTraining, ILearningRate
    {
        /// <summary>
        /// The network being trained.
        /// </summary>
        ///
        private readonly CPNNetwork network;

        /// <summary>
        /// The training data. This is unsupervised training, so only the input
        /// portion of the training data will be used.
        /// </summary>
        ///
        private readonly MLDataSet training;

        /// <summary>
        /// The learning rate.
        /// </summary>
        ///
        private double learningRate;

        /// <summary>
        /// If the weights have not been initialized, then they can be initialized
        /// before training begins. This will be done on the first iteration.
        /// </summary>
        ///
        private bool mustInit;

        /// <summary>
        /// Construct the instar training object.
        /// </summary>
        ///
        /// <param name="theNetwork">The network to be trained.</param>
        /// <param name="theTraining">The training data.</param>
        /// <param name="theLearningRate">The learning rate.</param>
        /// <param name="theInitWeights">training elements as instar neurons.</param>
        public TrainInstar(CPNNetwork theNetwork, MLDataSet theTraining,
                           double theLearningRate, bool theInitWeights) : base(TrainingImplementationType.Iterative)
        {
            network = theNetwork;
            training = theTraining;
            learningRate = theLearningRate;
            mustInit = theInitWeights;
        }

        /// <inheritdoc />
        public override sealed bool CanContinue
        {
            get { return false; }
        }


        /// <inheritdoc />
        public override MLMethod Method
        {
            get { return network; }
        }

        #region ILearningRate Members

        /// <inheritdoc />
        public double LearningRate
        {
            get { return learningRate; }
            set { learningRate = value; }
        }

        #endregion

        /// <summary>
        /// Approximate the weights based on the input values.
        /// </summary>
        ///
        private void InitWeights()
        {
            if (training.Count != network.InstarCount)
            {
                throw new NeuralNetworkError(
                    "If the weights are to be set from the "
                    + "training data, then there must be one instar "
                    + "neuron for each training element.");
            }

            int i = 0;

            foreach (MLDataPair pair  in  training)
            {
                for (int j = 0; j < network.InputCount; j++)
                {
                    network.WeightsInputToInstar[j, i] =
                        pair.Input[j];
                }
                i++;
            }
            mustInit = false;
        }

        /// <inheritdoc />
        public override sealed void Iteration()
        {
            if (mustInit)
            {
                InitWeights();
            }

            double worstDistance = Double.NegativeInfinity;


            foreach (MLDataPair pair  in  training)
            {
                MLData xout = network.ComputeInstar(pair.Input);

                // determine winner
                int winner = EngineArray.IndexOfLargest(xout.Data);

                // calculate the distance
                double distance = 0;
                for (int i = 0; i < pair.Input.Count; i++)
                {
                    double diff = pair.Input[i]
                                  - network.WeightsInputToInstar[i, winner];
                    distance += diff*diff;
                }
                distance = BoundMath.Sqrt(distance);

                if (distance > worstDistance)
                {
                    worstDistance = distance;
                }

                // train
                for (int j = 0; j < network.InputCount; j++)
                {
                    double delta = learningRate
                                   *(pair.Input[j] - network.WeightsInputToInstar[j, winner]);

                    network.WeightsInputToInstar.Add(j, winner, delta);
                }
            }

            Error = worstDistance;
        }

        /// <inheritdoc />
        public override sealed TrainingContinuation Pause()
        {
            return null;
        }

        /// <inheritdoc />
        public override void Resume(TrainingContinuation state)
        {
        }
    }
}