using Laboratory5.Helpers;
using Laboratory5.Models;
using System.Web.Http;

namespace Laboratory5.Controllers
{
    public class MatrixMultiplyController : ApiController
    {
        public MultiplicationResponse Get([FromUri]int a, [FromUri]int b, [FromUri]int c)
        {
            var result = MatrixMultiplicationHelper.InternalMultiplication(a,b,c);
            return result;
        }

        public object Post([FromBody]MultiplicationRequest multiplicationRequest)
        {
            var result = MatrixMultiplicationHelper.MultiplyMatrices(multiplicationRequest.FirstArray, multiplicationRequest.SecondArray);
            return result;
        }
    }
}
