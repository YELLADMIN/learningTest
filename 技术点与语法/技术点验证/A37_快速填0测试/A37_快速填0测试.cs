using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Verification.Core;

namespace 技术点验证.A37_快速填0测试
{
    [VerifcationType(VerificationTypeEnum.A37_快速填0测试)]
    public class A37_快速填0测试 : IVerification
    {
        public void Start(string[]? args)
        {
            Type[] types = new Type[]
            {
                typeof(QuickFillZero)
            };
            BenchmarkSwitcher.FromTypes(types).RunAll();
        }
    }

    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class QuickFillZero
    {
        [Params(10 * 1000, 30 * 1000)]
        public int DataCount;

        public int[] testBytes;

        [GlobalSetup]
        public void SetUp()
        {
            this.testBytes = new int[this.DataCount];
            Random random = new Random();
            for (int i = 0 ; i < this.testBytes.Length ; i++)
            {
                this.testBytes[i] = random.Next(1, 8);
            }
        }

        [Benchmark(Baseline = true)]
        public long UseIndex(byte[] bytes)
        {
            byte[] zeroBytes = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 1 ; i < bytes.Length + 1 ; i++)
            {
                zeroBytes[^i] = bytes[^i];
            }

            return BitConverter.ToInt64(zeroBytes);
        }

        [Benchmark]
        public void UseEnumerable(byte[] bytes)
        {
            IEnumerable<byte>? aaa = Enumerable.Repeat((byte)0, 8 - bytes.Length)
                 .Concat(bytes);

            bytes.AsSpan():

            //ReadOnlySpan
            new ReadOnlySpan<byte>();
        }

        public void TestBase()
        {
        }
    }
}