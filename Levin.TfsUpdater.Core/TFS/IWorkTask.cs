namespace Levin.TfsUpdater.Core
{
    public interface IWorkTask
    {
        string GetDescription();

        void ReplaceDescriprion(string newdDescription);
    }
}