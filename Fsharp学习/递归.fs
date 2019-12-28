﻿module Fsharp学习.递归

open System
open System.Collections.Generic

///演示递归
let showRecursiveFunctions=

    //处理元素的集合或序列通常是通过F＃中的递归来完成的。
    //尽管F＃支持循环和命令式编程，但首选递归，因为它更容易保证正确性。

    printfn "演示---递归"

    ///阶乘- f(n)=n*f(n-1)
    let rec factortial n=
        if n=0 then 1
        else n*factortial(n-1)

    ///阶乘-尾递归优化
    let rec factortial2 (n:int,runing_total:int) =
        if n=0 then runing_total //在n=0时跳出 0的乘为1
        elif runing_total=0 then factortial2( n-1 , 1*n )
        else factortial2( n-1 , runing_total*n )

    ///阶乘-尾递归优化
    let factortial2(n:int)=
        factortial2(n,0)

    printfn "%d的阶乘:%d" 6 (factortial(6))
    printfn "%d的阶乘2:%d" 6 (factortial2(6))

    ///计算2个数的最大公因数
    //由于所有递归调用都是尾调用，因此编译器会将函数转换为循环，从而提高了性能并减少了内存消耗。
    let rec greatestCommonFactor a b =
          if a = 0 then b
          elif a < b then greatestCommonFactor a (b - a)
          else greatestCommonFactor (a - b) b
    printfn ""

    //斐波那契数列
    //f(1)=1
    //f(2)=1
    //f(3)=2 f(2)+f(1)
    //f(4)=3 f(3)+f(2)
    //       f(2)+f(2)+f(1)
    //
    //f(5)=5 f(4)+f(3)
    //       f(2)+f(2)+f(1) +   f(2)+f(1)
    //

    ///斐波那契数列- F(1)=1，F(2)=1, F(n)=F(n-1)+F(n-2)（n>=3，n∈N*）
    let rec fibonacciSequence (n:int)=
       if n<=2 then 1
       else fibonacciSequence(n-1)+fibonacciSequence(n-2)

    ///斐波那契数列-尾递归
    let rec fibonacciSequenceByTailRecursion (n:int,first:int,second:int)=
        if   n=1 then first
        elif n=2 then second
        else fibonacciSequenceByTailRecursion(n-1,second,first+second)

    let fibonacciSequenceByTailRecursion n =fibonacciSequenceByTailRecursion(n,1,1) //这里2个1为种子。 f(1)=1 f(2)=1
    let firstFibonacciSequence n=  Seq.init (n+1) fibonacciSequenceByTailRecursion
                                    |>Seq.skip 1
                                    |>Seq.toList
    printfn "斐波那契数列前%d项:%A" 5 (firstFibonacciSequence(5))

     ///斐波那契数列-动态规划
    let rec fibonacciSequenceByDynamicProgramming (n:int,map:Dictionary<int, int>)=
        if map.ContainsKey(n) then map.[n]

        elif n<=2
            then
            map.[n]<-1
            1
        else
            let result=fibonacciSequenceByDynamicProgramming(n-1,map)+fibonacciSequenceByDynamicProgramming(n-2,map)
            map.[n]<-result
            result

    let dict = new Dictionary<int, int>()
    dict.Add(1,1)
    dict.Add(2,1)

    //let firstFibonacciSequence2222 (n,dict)=Seq.init>>fibonacciSequenceByDynamicProgramming(n,dict)

    //let firstFibonacciSequence2 (n,dict)=  Seq.init n+1 fibonacciSequenceByTailRecursion dict
    //                                |>Seq.skip 1
    //                                |>Seq.toList

    printfn "斐波那契数列前%d项:%A" 5 