namespace CKeditor.CKEditorModule
{
    public interface IGenricRepository<T> where T : class
    {
        public bool InsertRecord(T entity);
    }
}
