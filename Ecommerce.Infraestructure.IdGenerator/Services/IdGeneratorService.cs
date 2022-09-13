using IdGen;
using System.Reflection.Emit;

namespace Ecommerce.Infraestructure.IdGenerator.Services
{
    public interface IIdGeneratorService
    {
        List<long> Generate(int count);
        long Generate();
    }

    public class IdGeneratorService : IIdGeneratorService
    {
        private IdGen.IdGenerator _generator;
        public IdGeneratorService()
        {
            var structure = new IdStructure(48, 2, 13);
            var options = new IdGeneratorOptions(structure);
            _generator = new IdGen.IdGenerator(1, options);
        }
        public List<long> Generate(int count)
        {
            return _generator.Take(count).ToList();
        }

        public long Generate()
        {
            return _generator.CreateId();
        }
    }
}
