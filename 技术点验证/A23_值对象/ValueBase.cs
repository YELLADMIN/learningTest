﻿using System;
using Verification.Core;

namespace 技术点验证
{
    /// <summary>
    /// 值对象基类-用于值类型非class<para></para>
    /// 可保证数据不能为null，数据正确(需重写<see cref="BizCheckValue"/>方法以完成业务规则检测)<para></para>
    /// 使用时可以当普通数据使用，如: int a=new Age(50)
    /// </summary>
    /// <remarks>
    /// 微软关于值对象的资料:https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    public abstract class ValueBase<TValue> : IValue<TValue>
    {
        public TValue Value { get; }

        #region 内部逻辑

        protected ValueBase(TValue data)
        {
            this.Value = data;

            if (this.IsNulReference(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            var checkObj = this.Check();
            if (!checkObj.checkResult)
            {
                throw new ArgumentException(checkObj.errorObj.ErrorMessage, nameof(data));
            }
        }

        /// <summary>
        /// 检查空引用。值类型与非空则返回false，引用类型为空时返回true
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsNulReference(TValue data)
        {
            if (typeof(TValue).IsValueType)
            {
                return false;
            }
            else
            {
                return data == null;
            }
        }

        /// <summary>
        /// 检查数据<para></para>
        /// 检查通过返回true，否则返回false+错误对象
        /// </summary>
        /// <returns></returns>
        public virtual (bool checkResult, BizError errorObj) Check()
        {
            if (this.BizCheckValue())
            {
                return (
                    true,
                    new BizError());
            }
            else
            {
                return (false, this.GetBizError());
            }
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case object _ when object.ReferenceEquals(this, obj):
                    return true;

                case IValue<TValue> obj2:
                    return this.EqualsCode(obj2.Value);

                case TValue obj2:
                    return this.EqualsCode(obj2);

                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            return this.GetHashCodeCore();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        #endregion 内部逻辑

        #region 由子类重写

        /// <summary>
        /// 由子类重写，指示如何进行业务检查。true为检查通过，否则为false
        /// </summary>
        /// <returns></returns>
        public abstract bool BizCheckValue();

        #endregion 由子类重写

        #region 子类可重写

        /// <summary>
        /// 子类可重写，指示当业务检查失败时，异常中的错误信息。
        /// </summary>
        /// <param name="value">发生错误的值</param>
        /// <returns></returns>
        public virtual string ErrorMsgForCheckValue(TValue value) => $"Check {this.GetType().Name} value error. now value:{value.ToString()}";

        /// <summary>
        /// 子类可重写，指示如果使用<paramref name="value"/>与<see cref="Value"/>进行内容比较<para></para>
        /// 引用、类型比较由基类完成<para></para>
        /// 基类默认行为：用<see cref="object.Equals(object)"/>方法对<paramref name="value"/>、<see cref="Value"/>做相等比较
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool EqualsCode(TValue value) => object.Equals(this.Value, value);

        /// <summary>
        /// 子类可重写，指示如果使用<see cref="Value"/>计算哈希值<para></para>
        /// 基类默认行为：用<see cref="TValue.GetHashCode"/>方法对<see cref="Value"/>做计算
        /// </summary>
        /// <returns></returns>
        public virtual int GetHashCodeCore() => this.Value.GetHashCode();

        #endregion 子类可重写

        #region 子类可用的方法

        /// <summary>
        /// 获取一个<see cref="BizError"/>对象
        /// </summary>
        /// <returns></returns>
        protected virtual BizError GetBizError()
        {
            return new BizError()
            {
                ErrorCode = "0OO0",
                CustomObject = this,
                ErrorMessage = this.ErrorMsgForCheckValue(this.Value)
            };
        }

        #endregion 子类可用的方法

        #region 符号重载

        //== !=必须调用Equals方法。因为 vo1==null 这种也是调用一个等号运算符，导致死循环

        public static bool operator ==(ValueBase<TValue> v1, ValueBase<TValue> v2)
        {
            return object.Equals(v1, v2);//具体比较逻辑由Equals完成
        }

        public static bool operator !=(ValueBase<TValue> v1, ValueBase<TValue> v2)
        {
            return !object.Equals(v1, v2);// 具体比较逻辑由Equals完成
        }

        #region 隐式和显式转换

        /// <summary>
        /// 隐式转换  <![CDATA[ValueObject<T, TValue>->TValue   eg: TValue a=new ValueObject<TValue>(xxx)]]>
        /// </summary>
        /// <param name="valueObject"></param>
        public static implicit operator TValue(ValueBase<TValue> valueObject)
        {
            return valueObject.Value;
        }

        /*
         * 显式转换在这个基础类里没办法写，原因：
         *  TValue b = (TValue)(new ValueObject<TValue>(xxx)); 这种代码在编译时 (TValue)这个强转的就被编译器拿掉了。
         *
         * TValue b = (double)(new ValueObject<TValue>(xxx)); 而这种强转，也不可能在基础类里把所有的都 写进去
         *
         */

        #endregion 隐式和显式转换

        #endregion 符号重载
    }
}