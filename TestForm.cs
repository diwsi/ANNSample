/// <summary>
///  Artificial neural network  and backpropagation example
/// Engin Özdemir 2016
/// xenamorphx@gmail.com
/// https://binarysongs.blogspot.com
/// </summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class TestForm : Form
    {
        public NeuralNetwork network;
        public List<double[]> trainingData;
        Random rnd;
        int trainedTimes;
        public TestForm()
        {
            InitializeComponent();

            //For debugging
            int Seed = 1923;

            // 2 input neurons 2 hidden layers with 3 and 2 neurons and 1 outpu neuron
            network = new NeuralNetwork(2, new int[] { 3, 3 }, 1, null, Seed);

            //Generate Random Training Data 
            trainingData = new List<double[]>();
            rnd = new Random(Seed);

            var trainingDataSize = 75;
            for (int i = 0; i < trainingDataSize; i++)
            {
                var input1 = Math.Round(rnd.NextDouble(), 2); //input 1
                var input2 = Math.Round(rnd.NextDouble(), 2); // input 2
                var output = (input1+input2)/2 ; // output as avarage of inputs
                trainingData.Add(new double[] { input1, input2, output });// Training data set                
                chart1.Series[0].Points.AddXY(i, output);                
            }            
        }

        public void Train(int times)
        {
            //Train network x0 times  
            for (int i = 0; i < times; i++)
            {
                //shuffle list for better training
                var  shuffledTrainingData = trainingData.OrderBy(d => rnd.Next()).ToList();               
                List<double> errors = new List<double>();
                foreach (var item in shuffledTrainingData)
                {
                    var inputs = new double[] { item[0], item[1] };
                    var output = new double[] { item[2] };

                    //Train current set
                    network.Train(inputs, output);

                    errors.Add(network.GlobalError);
                }                

            }
            chart1.Series[1].Points.Clear();
            for (int i = 0; i < trainingData.Count; i++)
            {
                var set = trainingData[i];

                chart1.Series[1].Points.AddXY(i, network.FeedForward(new double[] { set[0], set[1] })[0]);
            }
            trainedTimes += times;
            TrainCounterlbl.Text = string.Format("Trained {0} times", trainedTimes);
        }

        private void Trainx1_Click(object sender, EventArgs e)
        {
            Train(1);
        } 

        private void Trainx50_Click(object sender, EventArgs e)
        {
            Train(50);
        }

        private void Trainx500_Click(object sender, EventArgs e)
        {
            Train(500);
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            var testData = new double[] { rnd.NextDouble(), rnd.NextDouble() };
            var result = network.FeedForward(testData)[0];
            MessageBox.Show(string.Format("Input 1:{0} {4} Input 2:{1} {4} Expected:{3}  Result:{2} {4}",
                format(testData[0]),
                format(testData[1]), 
                format(result),
                format((testData[0]+ testData[1])/2), 
                Environment.NewLine));
        }


        string format(double val)
        {
            return val.ToString("0.000");
        }
    }


    public class Layer
    {
        public Neuron[] Neurons { get; set; }
    }

    public class Synapse
    {
        public double Weight { get; set; }
        public Neuron Target { get; set; }
        public Neuron Source { get; set; }
        public double PreDelta { get; set; }
        public double Gradient { get; set; }
        public Synapse(double weight, Neuron target, Neuron source)
        {
            Weight = weight;
            Target = target;
            Source = source;
        }

    }

    public enum NeuronTypes
    {
        Input,
        Hidden,
        Output
    }

    public class Neuron
    {
        public List<Synapse> Inputs { get; set; }
        public List<Synapse> Outputs { get; set; }
        public double Output { get; set; }
        public double TargetOutput { get; set; }
        public double Delta { get; set; }
        public double Bias { get; set; }
        int? maxInput { get; set; }
        public NeuronTypes NeuronType { get; set; }

        public Neuron(NeuronTypes neuronType, int? maxInput)
        {
            this.NeuronType = neuronType;
            this.maxInput = maxInput;
            this.Inputs = new List<Synapse>();
            this.Outputs = new List<Synapse>();
        }

        public bool AcceptConnection
        {
            get
            {
                return !(NeuronType == NeuronTypes.Hidden && maxInput.HasValue && Inputs.Count > maxInput);

            }
        }

        public double InputSignal
        {
            get
            {
                return Inputs.Sum(d => d.Weight * (d.Source.Output + Bias));
            }
        }

        public double BackwardSignal()
        {
            if (Outputs.Any())
            {
                Delta = Outputs.Sum(d => d.Target.Delta * d.Weight) * activatePrime(Output);
            }
            else
            {
                Delta = (Output - TargetOutput) * activatePrime(Output);
            }

            return Delta + Bias;
        }

        public void AdjustWeights(double learnRate, double momentum)
        {
            if (Inputs.Any())
            {
                foreach (var synp in Inputs)
                {

                    var adjustDelta = Delta * synp.Source.Output;
                    synp.Weight -= learnRate * adjustDelta + synp.PreDelta * momentum;
                    synp.PreDelta = adjustDelta;

                }
            }
        }

        public double ForwardSignal()
        {
            Output = activate(InputSignal);
            return Output;
        }

        double activatePrime(double x)
        {
            return x * (1 - x);
        }

        double activate(double x)
        {
            return 1 / (1 + Math.Pow(Math.E, -x));
        }
    }

    public class NeuralNetwork
    {
        public double LearnRate = .5;
        public double Momentum = .3;
        public List<Layer> Layers { get; private set; }
        int? maxNeuronConnection;
        public int? Seed { get; set; }
        public NeuralNetwork(int inputs, int[] hiddenLayers, int outputs, int? maxNeuronConnection = null, int? seed = null)
        {
            this.Seed = seed;
            this.maxNeuronConnection = maxNeuronConnection;
            this.Layers = new List<Layer>();
            buildLayer(inputs, NeuronTypes.Input);
            for (int i = 0; i < hiddenLayers.Length; i++)
            {
                buildLayer(hiddenLayers[i], NeuronTypes.Hidden);
            }
            buildLayer(outputs, NeuronTypes.Output);
            InitSnypes();

        }

        void buildLayer(int nodeSize, NeuronTypes neuronType)
        {
            var layer = new Layer();
            var nodeBuilder = new List<Neuron>();
            for (int i = 0; i < nodeSize; i++)
            {
                nodeBuilder.Add(new Neuron(neuronType, maxNeuronConnection));
            }
            layer.Neurons = nodeBuilder.ToArray();
            Layers.Add(layer);
        }

        private void InitSnypes()
        {
            var rnd = Seed.HasValue ? new Random(Seed.Value) : new Random();

            for (int i = 0; i < Layers.Count - 1; i++)
            {
                var layer = Layers[i];
                var nextLayer = Layers[i + 1];
                foreach (var node in layer.Neurons)
                {
                    node.Bias = 0.1 * rnd.NextDouble();
                    foreach (var nNode in nextLayer.Neurons)
                    {
                        if (!nNode.AcceptConnection) continue;
                        var snypse = new Synapse(rnd.NextDouble(), nNode, node);
                        node.Outputs.Add(snypse);
                        nNode.Inputs.Add(snypse);
                    }
                }
            }

        }

        public double GlobalError
        {
            get
            {
                return Math.Round(Layers.Last().Neurons.Sum(d => Math.Pow(d.TargetOutput - d.Output, 2) / 2), 4);
            }
        }

        public void BackPropagation()
        {
            for (int i = Layers.Count - 1; i > 0; i--)
            {
                var layer = Layers[i];
                foreach (var node in layer.Neurons)
                {
                    node.BackwardSignal();
                }
            }
            for (int i = Layers.Count - 1; i >= 1; i--)
            {
                var layer = Layers[i];
                foreach (var node in layer.Neurons)
                {
                    node.AdjustWeights(LearnRate, Momentum);
                }
            }
        }

        public double[] Train(double[] _input, double[] _outputs)
        {
            if (_outputs.Count() != Layers.Last().Neurons.Count() || _input.Any(d => d < 0 || d > 1) || _outputs.Any(d => d < 0 || d > 1))
                throw new ArgumentException();

            var outputs = Layers.Last().Neurons;
            for (int i = 0; i < _outputs.Length; i++)
            {
                outputs[i].TargetOutput = _outputs[i];
            }

            var result = FeedForward(_input);

            BackPropagation();
            return result;
        }

        public double[] FeedForward(double[] _input)
        {
            if (_input.Count() != Layers.First().Neurons.Count())
                throw new ArgumentException();


            var InputLayer = Layers.First().Neurons;
            for (int i = 0; i < _input.Length; i++)
            {
                InputLayer[i].Output = _input[i];
            }

            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                foreach (var node in layer.Neurons)
                {
                    node.ForwardSignal();
                }
            }

            return Layers.Last().Neurons.Select(d => d.Output).ToArray();
        }
    }
}
