﻿using System;

namespace 反射验证
{
    public class Generic<T>
    {
        public Generic()
        {
            Console.WriteLine("T={0}", typeof(T));
        }
    }

    public class Generic2<T, U>
    {
        public Generic2()
        {
            Console.WriteLine("T={0},U={1}", typeof(T), typeof(U));
        }
    }

    internal class Test
    {
        private static void Main()
        {
            Console.WriteLine("验证开始");

            #region 1.获取泛型结构

            //Type genericClass = typeof(Generic<>);//获取基本泛型结构
            //Type genericClass2 = typeof(Generic2<,>);//实例获取双参数基本泛型结构

            ////1.1 用参数结构制作实参泛型结构
            //string typeName = "System.String";
            //Type typeArgument = Type.GetType(typeName);//泛型形参结构
            //Type constructedClass = genericClass.MakeGenericType(typeArgument);
            //object created = Activator.CreateInstance(constructedClass);

            #endregion 1.获取泛型结构

            #region 2.确定结构是否为泛型

            ////IsGenericType 结构是否为泛型
            ////IsGenericTypeDefinition 结构是滞为泛型定义

            //Type strType = typeof(string);
            //Type genericType = typeof(Generic<>);
            //Type genericType2 = genericType.MakeGenericType(strType);

            //Console.WriteLine($"strType-IsGenericType\t{strType.IsGenericType}");//False
            //Console.WriteLine($"strType-IsGenericTypeDefinition\t{strType.IsGenericTypeDefinition}");//False

            //Console.WriteLine($"genericType-IsGenericType\t{genericType.IsGenericType}");//True
            //Console.WriteLine($"genericType-IsGenericTypeDefinition\t{genericType.IsGenericTypeDefinition}");//True

            //Console.WriteLine($"genericType2-IsGenericType\t{genericType2.IsGenericType}");//True
            //Console.WriteLine($"genericType2-IsGenericTypeDefinition\t{genericType2.IsGenericTypeDefinition}");//False

            #endregion 2.确定结构是否为泛型

            #region 3.获取泛型参数数组
            //{type}.GetGenericArguments() 


            Type strType = typeof(string);
            Type genericType = typeof(Generic<>);
            Type genericType2 = genericType.MakeGenericType(strType);

            Type[] typeArry = new Type[] { strType, genericType, genericType2 };

            //遍历上面3个
            foreach (var itemA in typeArry)
            {
                //遍历每个结构中的参数
                //Generic<string> 会包括2个  它自己和string的
                Console.WriteLine($"TypeName:  {itemA.FullName}");
                foreach (Type item in itemA.GetGenericArguments())
                {
                    Console.WriteLine(item.FullName);
                }
                Console.WriteLine();
                Console.WriteLine("-----------");
            }

            #endregion 3.获取泛型参数数组

            Console.WriteLine("验证结束");
            Console.ReadLine();
        }
    }
}