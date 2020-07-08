﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Verification.Core;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A31_快速转换枚举为字典)]
    public class A31_快速转换枚举为字典 : IVerification
    {
        public void Start(string[] args)
        {
            Type[] types = new Type[]
            {
                typeof(EnumTodic)
            };
            BenchmarkSwitcher.FromTypes(types).RunAll();
        }
    }

    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class EnumTodic
    {
        [Params(10 * 1000, 30 * 1000)]
        public int DataCount;

        [Benchmark(Baseline = true)]
        public void Reflection()
        {
            FieldInfo[] fields = typeof(VerificationTypeEnum)
                 .GetFields(BindingFlags.Static | BindingFlags.Public)
                 ?? Array.Empty<FieldInfo>();

            _ = fields.ToDictionary(k => k.Name, v => (int)v.GetValue(null));
        }

        [Benchmark]
        public void SystemMothod()
        {
            _ = Enum.GetValues(typeof(VerificationTypeEnum))
                .Cast<VerificationTypeEnum>()
                .ToDictionary(k => k.ToString(), v => v);
        }
    }
}