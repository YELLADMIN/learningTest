﻿using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System;
using System.Reflection;

namespace Redis和序列化研究
{
    public class MessagePack基准测试
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("测试开始");

            //Summary summary = BenchmarkRunner.Run<TestCase_Serialize>();

            BenchmarkSwitcher.FromAssembly(typeof(MessagePack基准测试).Assembly).RunAll();


            //var tt = new TestCase_Serialize();
            //tt.SetUp();
            //tt.TestIntSerialize();
            //tt.TestStringSerialize();
            //tt.TestNonSerialize();
            //tt.TestDefaultSerialize();
            //tt.TestJsonSerialize();

            //兼容性 测试
            //CompatibilityTest();

            Console.WriteLine("测试结束");
            Console.ReadLine();
        }

        private static void CompatibilityTest()
        {
            Serializable.StartupSerializable();//初始化序列化器

            TestEntity_Non result = null;

            // 测试intKey To NonKey
            TestEntity_Int test = new TestEntity_Int()
            {
                TestEnum = TestEnum.Close
            };
            result = BaseTest(test);

            //测试匿名类型+枚举值
            dynamic test2 = new { TestEnum = 20 };
            result = BaseTest(test2);

            //测试枚举名-错误
            //dynamic test3 = new { TestEnum = "Close" };
            //result = BaseTest(test3);

            //测试0 1向bool转换--错误
            //dynamic test4 = new { Boolen = 0 };
            //result = BaseTest(test4);

            //测试true向bool转换
            //错误
            //dynamic test5 = new { Boolen = "True" };
            //result = BaseTest(test5);

            //错误
            //dynamic test55 = new { Boolen = "true" };
            //result = BaseTest(test55);

            dynamic test555 = new { Boolen = true };
            result = BaseTest(test555);

            //测试数值类型之间转换

            //错误
            dynamic test6 = new { Integr = 10.00 };
            result = BaseTest(test6);

            //错误
            //dynamic test66 = new { Double = 20.02M };
            //result = BaseTest(test66);

            //错误
            //dynamic test666 = new {  Decimal = 30 };
            //result = BaseTest(test666);

            dynamic test6666 = new { Decimal = 30M };
            result = BaseTest(test6666);
        }

        public static TestEntity_Non BaseTest<T>(T obj)
        {
            var byteT = Serializable.Serialize(obj);
            var result = Serializable.Deserialize<TestEntity_Non>(byteT);
            return result;
        }
    }
}