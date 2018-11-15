﻿using Nova.LocalChain;
using Nova.LocalChain.Entity;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 逻辑链扩展
    /// </summary>
    public static class NovaExtension
    {
        /// <summary>
        /// 注册逻辑链服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNova(this IServiceCollection services)
        {
            //扫描并注册程序集中所有带标签的实现
            List<TaskEntity> taskList = NovaHelper.FindAllTaskEntity();
            foreach (var item in taskList)
            {
                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(
                     item.StepType,
                    item.StepType,
                    item.Attribute.Lifetime);

                services.Add(serviceDescriptor);
            }

            //注册工厂接口
            services.AddSingleton<INovaFactory>(t =>
            {
                return new NovaFactory(t.GetService<IServiceProvider>(), taskList);
            });

            return services;
        }
    }
}