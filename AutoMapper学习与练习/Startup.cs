﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper学习与练习.Orders;
using AutoMapper学习与练习.Orders.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutoMapper学习与练习
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 添加Mapper映射关系
        /// </summary>
        /// <param name="services"></param>
        public static void AddMapper( IServiceCollection services )
        {
            var config = new AutoMapper.MapperConfiguration( cfg =>
            {
                cfg.AddProfile( new OrderMapFile( ) );
            } );

            var mapper = config.CreateMapper( );
            services.AddSingleton( mapper );
        }

        public static void AddIoc( IServiceCollection services )
        {
            services.AddScoped<IOrderServices , OrderServices>( );
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            AddMapper( services );
            AddIoc( services );

            services.AddMvc( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app , IHostingEnvironment env )
        {
            if( env.IsDevelopment( ) )
            {
                app.UseDeveloperExceptionPage( );
            }

            app.UseMvc( );
        }
    }
}