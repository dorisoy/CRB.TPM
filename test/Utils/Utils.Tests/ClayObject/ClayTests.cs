using CRB.TPM.Utils.ClayObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Utils.Tests.ClayObject
{
    /// <summary>
    /// 黏土对象-单元测试
    /// </summary>
    public class ClayTests
    {
        private readonly ITestOutputHelper _output;

        public ClayTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        /// <summary>
        /// 创建一个对象
        /// </summary>
        [Fact]
        public void Create_Clay()
        {
            // 创建一个空的粘土对象
            dynamic clay = new Clay();

            // 从现有的对象创建
            dynamic clay2 = Clay.Object(new { });

            // 从 json 字符串创建，可用于第三方 API 对接，非常有用
            dynamic clay3 = Clay.Parse(@"{""foo"":""json"", ""bar"":100, ""nest"":{ ""foobar"":true } }");

            Assert.True(clay.IsObject);
            Assert.True(clay2.IsObject);
            Assert.True(clay3.IsObject);
        }
        /// <summary>
        /// 读取/获取属性
        /// </summary>
        [Fact]
        public void SetOrGet_ClayProperty()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                }
            });

            string r1 = clay.Foo; // "json" - string类型
            double r2 = clay.Bar; // 100 - double类型
            bool r3 = clay.Nest.Foobar; // true - bool类型
            bool r4 = clay["Nest"]["Foobar"]; // 还可以和 Javascript 一样通过索引器获取

            Assert.Equal("json", r1);
            Assert.Equal(100, r2);
            Assert.True(r3);
            Assert.True(r4);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        [Fact]
        public void Add_ClayProperty()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                }
            });

            // 新增
            clay.Arr = new string[] { "NOR", "XOR" }; // 添加一个数组
            clay.Obj1 = new City { Name = "sc" }; // 新增一个实例对象
            clay.Obj2 = new { Foo = "abc", Bar = 100 }; // 新增一个匿名类

            Assert.True(clay.Arr.IsArray);
            Assert.Equal("sc", clay.Obj1.Name);
            Assert.Equal("abc", clay.Obj2.Foo);
        }

        /// <summary>
        /// 更新属性值
        /// </summary>
        [Fact]
        public void Update_ClayProperty()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                }
            });

            // 更新
            clay.Foo = "Furion";
            Assert.Equal("Furion", clay.Foo);

            clay["Nest"].Foobar = false;
            Assert.False(clay["Nest"].Foobar);

            clay.Nest["Foobar"] = true;
            Assert.True(clay["Nest"].Foobar);
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        [Fact]
        public void Delete_ClayProperty()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                },
                Arr = new string[] { "NOR", "XOR" }
            });

            // 删除操作
            clay.Delete("Foo"); // 通过 Delete 方法删除
            clay.Arr.Delete(0); // 支持数组 Delete 索引删除
            clay("Bar");    // 支持直接通过对象作为方法删除
            var res = clay.Arr(1);    // 支持数组作为方法删除

            Assert.False(res);
            Assert.False(clay.IsDefined("Foo"));
            Assert.False(clay.IsDefined("Bar"));
            Assert.Single(((object[])clay.Arr));
        }

        /// <summary>
        /// 判断属性是否存在
        /// </summary>
        [Fact]
        public void Exist_Clay()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                },
                Arr = new string[] { "NOR", "XOR" }
            });

            // 判断属性是否存在
            Assert.True(clay.IsDefined("Foo")); // true
            Assert.False(clay.IsDefined("Foooo")); // false
            Assert.True(clay.Foo()); // true
            Assert.False(clay.Foooo()); // false;
        }

        /// <summary>
        /// 判断属性是否存在
        /// </summary>
        [Fact]
        public void Each_Clay()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                },
                Arr = new string[] { "NOR", "XOR" }
            });

            var Arr = new string[] { "NOR", "XOR" };
            // 遍历数组
            foreach (string item in clay.Arr)
            {
                _output.WriteLine(item);
                Assert.Contains(item, Arr);
            }

            // 遍历整个对象属性及值，类似 JavaScript 的 for (var p in obj)
            foreach (KeyValuePair<string, dynamic> item in clay)
            {
                _output.WriteLine(item.Key + ":" + item.Value); // Foo:json, Bar: 100, Nest: { "Foobar":true}, Arr:["NOR","XOR"]
            }
        }

        /// <summary>
        /// 转换成具体对象
        /// </summary>
        [Fact]
        public void Convert_Clay()
        {
            dynamic clay = new Clay();
            clay.Arr = new string[] { "Furion", "Fur" };

            // 数组转换示例
            var a1 = clay.Arr.Deserialize<string[]>(); // 通过 Deserialize 方法
            var a2 = (string[])clay.Arr;    // 强制转换
            string[] a3 = clay.Arr; // 声明方式

            // 对象转换示例
            clay.City = new City { Id = 1, Name = "中山市" };
            var c1 = clay.City.Deserialize<City>(); // 通过 Deserialize 方法
            var c2 = (City)clay.City;    // 强制转换
            City c3 = clay.City; // 声明方式
        }

        /// <summary>
        /// 固化粘土
        /// </summary>
        [Fact]
        public void Solidify_Clay()
        {
            //固化粘土在很多时候和序列化很像，但是如果直接调用 Deserialize<object> 或 Deserialize<dynamic> 无法返回实际类型，所以就有了固化类型的功能

            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Amount = 100.66,
                CityPro = new City { Id = 1, Name = "中山市" }
            });

            // 返回 object
            var obj = clay.Solidify();

            // 返回 dynamic
            var obj1 = clay.Solidify<dynamic>();

            // 返回其他任意类型
            var obj2 = clay.CityPro.Solidify<City>();

            Assert.IsType(typeof(City), obj2);
        }

        /// <summary>
        /// 输出 JSON
        /// </summary>
        [Fact]
        public void Write_Json()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                },
                Arr = new string[] { "NOR", "XOR" }
            });

            // 输出 JSON
            var json = clay.ToString(); // "{\"Foo\":\"json\",\"Bar\":100,\"Nest\":{\"Foobar\":true},\"Arr\":[\"NOR\",\"XOR\"]}"
            _output.WriteLine(json);
        }

        /// <summary>
        /// 输出 XML 对象
        /// </summary>
        [Fact]
        public void Write_XML()
        {
            dynamic clay = Clay.Object(new
            {
                Foo = "json",
                Bar = 100,
                Nest = new
                {
                    Foobar = true
                },
                Arr = new string[] { "NOR", "XOR" }
            });

            // 输出 XElement
            var xml = clay.XmlElement;
            _output.WriteLine(xml.ToString());
        }
        /// <summary>
        /// 关键字处理
        /// </summary>
        [Fact]
        public void Handle_Keyword()
        {
            dynamic clay = new Clay();
            clay.@int = 1;
            clay.@event = "事件";

            Assert.Equal(1, clay.@int);
            Assert.Equal("事件", clay.@event);
        }

        /// <summary>
        /// 转换成字典类型
        /// </summary>
        [Fact]
        public void Convert_Dictionary()
        {
            dynamic clay = Clay.Object(new { name = "张三" });
            clay.name = "李四";
            Dictionary<string, object> parms = clay.ToDictionary();
            Assert.Equal("李四", parms["name"]);
        }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
