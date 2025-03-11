// 데이터 선언부
namespace DataDeclaration
{
    #region Enum

    public enum ResourceType
    {
        Animations,
        Materials,
        Prefabs,
        Textures
    }

    public enum PrefabType
    {
        Character,
        Object,
        UI
    }
    
    public enum ItemType
    {
        Recoverable,
        Sign
    }

    public enum ConditionType
    {
        Stamina,
        SlowTime,
    }

    public enum UIState
    {
        Main,
        Menu
    }
    #endregion
    
    #region Interface
    /// <summary>
    /// 상호작용 오브젝트에 상속
    /// </summary>
    public interface IInteraction
    {
        /// <summary>
        /// 상호작용 시 실행할 로직 구현
        /// </summary>
        public void OnInteract();
    }
    #endregion
}
