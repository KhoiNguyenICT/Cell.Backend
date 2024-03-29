﻿using System;
using AutoMapper;

namespace Cell.Core.Extensions
{
    public static class AutoMapperExtension
    {
        private static IMapper _mapper;

        public static IMapper RegisterMap(this IMapper mapper)
        {
            _mapper = mapper;
            return mapper;
        }

        public static T To<T>(this object source)
        {
            return _mapper.Map<T>(source);
        }

        public static object To(this object source, Type destinationType)
        {
            return _mapper.Map(source, source.GetType(), destinationType);
        }

        public static T To<T>(this object source, Action<IMappingOperationOptions> options)
        {
            return _mapper.Map<T>(source, options);
        }

        public static T To<T>(this object source, T dest)
        {
            return _mapper.Map(source, dest);
        }
    }
}
