namespace SerializeDeserializeDemo.Models
{
    public class TestRequestDTO
    {
        public int Id { get; set; }
        public string Description {  get; set; }
        public List<TestModalDTO> ModalList { get; set; }
        public List<List<TestModalDTO>> ModalListList { get; set; }
        public Dictionary<string,TestModalDTO> ModalDictionary { get; set; }
        public Dictionary<string,Dictionary<string,TestModalDTO>> ModalDictionaryDictionary { get; set; }
    }
}
