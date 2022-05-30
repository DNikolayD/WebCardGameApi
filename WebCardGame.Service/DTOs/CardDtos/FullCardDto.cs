namespace WebCardGame.Service.DTOs.CardDTOs
{
    public class FullCardDto : BaseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageId { get; set; }

        public int TypeId { get; set; }

        public int Cost { get; set; }

        public string[] EffectIds { get; set; }

        public string CreatorId { get; set; }

        public string? DeckId { get; set; }

    }
}
