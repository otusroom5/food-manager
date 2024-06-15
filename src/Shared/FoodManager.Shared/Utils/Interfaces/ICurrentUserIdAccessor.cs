using System;

namespace FoodManager.Shared.Utils.Interfaces;

public interface ICurrentUserIdAccessor
{
    Guid GetCurrentUserId();
}
