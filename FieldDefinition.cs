namespace CVP.Routines.MotorArquivosComunicacao.Console
{
    public class FieldDefinition
    {
        public string Name { get; }
        public int Offset { get; }
        public int Size { get; }

        public FieldDefinition(string name, int offset, int size)
        {
            Name = name;
            Offset = offset;
            Size = size;
        }
    }
}
