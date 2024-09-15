// 0.0.1
namespace Core.Input
{
    public interface IInputAction<T>
    {
        public bool IsActionInvoked();
        public T GetInputValue();
    }
}