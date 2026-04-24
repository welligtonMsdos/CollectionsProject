namespace CollectionApplication.Dtos;

public record ConcertDto(Guid Guid,
                         string Artist,
                         string Venue,
                         DateOnly ShowDate,
                         string Photo,
                         string ShowDateDescription);

public record ConcertCreateDto(string Artist,
                               string Venue,
                               DateOnly ShowDate,
                               string Photo);

public record ConcertUpdateDto(string Artist,
                               string Venue,
                               DateOnly ShowDate,
                               string Photo);
