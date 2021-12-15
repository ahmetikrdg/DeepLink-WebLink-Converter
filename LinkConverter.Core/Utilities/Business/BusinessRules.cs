using LinkConverter.Core.Utilities.Results;

namespace LinkConverter.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;//I'm sending the wrong one
                }
            }
            return null;
        }
    }
}
