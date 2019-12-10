using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DynamicData;

namespace Designer.Core
{
    public class SourceListToEnumerableConverter<TInput, TDestination> : ITypeConverter<SourceList<TInput>, IEnumerable<TDestination>>
    {
        public IEnumerable<TDestination> Convert(SourceList<TInput> source, IEnumerable<TDestination> destination, ResolutionContext context)
        {
            return source.Items.Select(x => context.Mapper.Map<TDestination>(x)).ToList();
        }
    }
}