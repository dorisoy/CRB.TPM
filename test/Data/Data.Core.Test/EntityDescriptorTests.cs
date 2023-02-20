using Xunit;
using Xunit.Abstractions;

namespace Data.Core.Test
{
    public class EntityDescriptorTests : DbContextTests
    {
        public EntityDescriptorTests(ITestOutputHelper output) : base(output)
        {
        }

        //表名称测试
        [Fact]
        public void TableNameTest()
        {
            Assert.Equal("Article", _articleRepository.EntityDescriptor.TableName);
            Assert.Equal("MyCategory", _categoryRepository.EntityDescriptor.TableName);
        }
    }
}
