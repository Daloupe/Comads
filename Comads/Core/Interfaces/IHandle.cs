﻿using Core.SharedKernel;

namespace Core.Interfaces
{
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}