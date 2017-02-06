using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AutoMapper;

namespace ConsoleApp2
{
    public class Model
    {
        public ImmutableDictionary<string, string> Demo { get; set; }    
    }

    public class Dto
    {
        public Dictionary<string, string> Demo { get; set; }
    }

    public class ImmutableDictionaryConverter<TSource, TDestination> : ITypeConverter<ImmutableDictionary<TSource, TDestination>, IDictionary<TSource, TDestination>>
    {
        public IDictionary<TSource, TDestination> Convert(ImmutableDictionary<TSource, TDestination> source, IDictionary<TSource, TDestination> destination, ResolutionContext context)
        {
            return source?.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new MapperConfiguration(configure =>
            {
                //configure.CreateMap(typeof(ImmutableDictionary<,>), typeof(IDictionary<,>)).ConvertUsing(typeof(ImmutableDictionaryConverter<,>));
                configure.CreateMap<ImmutableDictionary<string, string>, IDictionary<string, string>>().ConvertUsing<ImmutableDictionaryConverter<string, string>>();
                configure.CreateMap<Model, Dto>();
            });

            var mapper = configuration.CreateMapper();

            var model = new Model()
            {
                Demo = ImmutableDictionary<string, string>.Empty.Add("Boo", "Yah")
            };

            var dto = mapper.Map<Dto>(model);

            Console.WriteLine($"Boo: {dto.Demo["Boo"]}");
        }
    }
}
