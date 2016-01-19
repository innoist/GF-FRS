using System.Linq;
using FRS.Models.MenuModels;

namespace FRS.Web.ModelMappers
{
    public static class CaresUserClaimsMapper
    {

        /// <summary>
        /// Create Permission key claim
        /// </summary>
        public static string CreatePermissionKey(this MenuRight source)
        {
            return null;
            //return source.Menu.PermissionKey;
        }

        public static MenuRight CreateFrom(this MenuRight source)
        {
            return new MenuRight
            {
                MenuRightId = source.MenuRightId,
                Menu_MenuId = source.Menu_MenuId,
                Role_Id = source.Role_Id,
                Menu = source.Menu.CreateFrom()
            };
        }
        /// <summary>
        /// Mapper used for Converting Menu in Menu Right
        /// </summary>
        public static Menu CreateFrom(this Menu source)
        {
            return new Menu
            {
                IsRootItem = source.IsRootItem,
                MenuId = source.MenuId,
                MenuFunction = source.MenuFunction,
                MenuImagePath = source.MenuImagePath,
                MenuKey = source.MenuKey,
                MenuTargetController = source.MenuTargetController,
                MenuTitle = source.MenuTitle,
                ParentItem = source.ParentItem != null ? source.ParentItem.Select(x => x.CreateFrom()).ToList() : null,
                ParentItem_MenuId = source.ParentItem_MenuId,
                PermissionKey = source.PermissionKey,
                SortOrder = source.SortOrder
            };
        }
    }

}
