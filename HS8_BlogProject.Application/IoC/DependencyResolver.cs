using Autofac;
using AutoMapper;
using HS8_BlogProject.Application.AutoMapper;
using HS8_BlogProject.Application.Services.AppUserService;
using HS8_BlogProject.Application.Services.AuthorService;
using HS8_BlogProject.Application.Services.CommentService;
using HS8_BlogProject.Application.Services.GenreService;
using HS8_BlogProject.Application.Services.LikeService;
using HS8_BlogProject.Application.Services.PostService;
using HS8_BlogProject.Domain.Repositories;
using HS8_BlogProject.Infrastructure.Repositories;

namespace HS8_BlogProject.Application.IoC
{
	public class DependencyResolver : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
			builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();

			builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
			builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
			
			builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
			builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();

			builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
			builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();

			builder.RegisterType<LikeService>().As<ILikeService>().InstancePerLifetimeScope();
			builder.RegisterType<LikeRepository>().As<ILikeRepository>().InstancePerLifetimeScope();

			builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
			builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();

			builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

			#region AutoMapper
			builder.Register(context => new MapperConfiguration(cfg =>
			{
				//Register Mapper Profile
				cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
			})).AsSelf().SingleInstance();

			builder.Register(c =>
			{
				//This resolves a new context that can be used later.
				var context = c.Resolve<IComponentContext>();
				var config = context.Resolve<MapperConfiguration>();
				return config.CreateMapper(context.Resolve);
			})
			.As<IMapper>()
			.InstancePerLifetimeScope();
			#endregion

			base.Load(builder);
		}
	}
}
