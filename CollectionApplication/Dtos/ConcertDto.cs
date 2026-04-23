namespace CollectionApplication.Dtos;

public record ConcertBaseDto(string Artist,
                             string Venue,
                             DateOnly ShowDate,
                             string Photo);

public record ConcertDto(Guid Guid,
                         ConcertBaseDto basedto,
                         string ShowDateDescription
                         );

public record ConcertCreateDto(ConcertBaseDto basedto);

public record ConcertUpdateDto(ConcertBaseDto basedto);
