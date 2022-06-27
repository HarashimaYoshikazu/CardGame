using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    List<InventryCard> _inventryCards = new List<InventryCard>();
    /// <summary>�C���x���g���̃J�[�h���X�g</summary>
    public List<InventryCard> InventryCards => _inventryCards;

    List<InventryCard> _decksCards = new List<InventryCard>();
    /// <summary>�f�b�L�̃J�[�h���X�g</summary>
    public List <InventryCard> DeckCards => _decksCards;
}
