using System.Collections.Generic;
using AutoMapper;
using DynamicData;

namespace Designer.Core
{
    public class SourceListConverter<TInput, TDestination> : ITypeConverter<IEnumerable<TInput>, SourceList<TDestination>>
    {
        public SourceList<TDestination> Convert(IEnumerable<TInput> source, SourceList<TDestination> destination, ResolutionContext context)
        {
            var sourceList = new SourceList<TDestination>();
            sourceList.Edit(x => x.AddRange(context.Mapper.Map<IEnumerable<TDestination>>(source)));

            return sourceList;
        }
    }
}