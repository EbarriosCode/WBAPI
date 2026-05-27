using WBAPI.Domain.Enums;

namespace WBAPI.Domain.Entities
{
    public class Album
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;  
        public string Artist { get; private set; } = string.Empty;
        public Genre Genre { get; private set; }
        public int Year { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        private Album() { } 
       
        public static Album Create(string name, string artist, Genre genre, int year)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(artist);

            return new Album
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Artist = artist.Trim(),
                Genre = genre,
                Year = year,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }
      
        public void Update(string name, string artist, Genre genre, int year)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(artist);

            Name = name.Trim();
            Artist = artist.Trim();
            Genre = genre;
            Year = year;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete() => IsActive = false;   // soft-delete
    }

}
