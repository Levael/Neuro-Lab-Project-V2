using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace MOCU
{
    
    public static class TrajectoryMaker
    {
        /// <summary>
        /// Array (1000 items from 0 to 1)
        /// </summary>
        public static Vector<double> CDF_VECTOR = CDF();
        private static Vector<double> CDF()
        {
            // I have no idea how it is calculated, but anyway
            // params are constant, don't change, they are fine
            // todo: maybe refactor later

            return TrajectoryMaker.GenerateGaussianSampledCDF(duration: 1.0, sigma: 3.0, magnitude: 1.0, frequency: 2000);
        }

        private static Vector<double> GenerateGaussianSampledCDF(double duration, double sigma, double magnitude, int frequency)
        {
            //generate a vector with one more point sampling the gaussian.
            Vector<double> returnedVector = CreateVector.Dense<double>((int)(frequency * duration) + 1, time => magnitude * Normal.CDF(duration / 2, duration / (2 * sigma), (double)time / frequency));
            for (int i = 1; i < returnedVector.Count; i++)
            {
                /*
                 * decrease from each sampled point the first point'
                 * so that the gaussian would begin with the a value that it's distance
                 * from the 0 would not be higher than the distance to the second point. i.e x0-0<x1-x0.
                 */
                returnedVector[i] -= returnedVector[0];
            }

            //remove the first point we need only for decreasing from other points.
            return returnedVector.SubVector(1, frequency);
        }

    }
}
