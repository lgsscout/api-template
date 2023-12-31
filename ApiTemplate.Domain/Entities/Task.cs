﻿using ApiTemplate.Domain.Entities.Common;

namespace ApiTemplate.Domain.Entities;

public class Task : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DoneAt { get; set; }

    public virtual User User { get; set; }
}