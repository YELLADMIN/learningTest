﻿namespace Verification.Core
{
    /// <summary>
    /// 业务验证中的元素
    /// </summary>
    public class BizError
    {
        /// <summary>
        /// 自定义状态
        /// </summary>
        public virtual object CustomObject { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public virtual string ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
		public virtual string ErrorMessage { get; set; }
    }
}