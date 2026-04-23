using CollectionApplication.Dtos;
using CollectionDomain.Entities;

namespace CollectionApplication.Extensions;

public static class ConcertExtensions
{
    private static string ToFormattedDateString(this DateOnly date)
    {
        return date.ToString(
            "dd 'de' MMMM 'de' yyyy",
            new System.Globalization.CultureInfo("pt-BR")
        );
    }

    public static ConcertDto ToConcertDto(this Concert concert)
    {
        ArgumentNullException.ThrowIfNull(concert);

        var basedto = new ConcertBaseDto
        (
            concert.Artist,
            concert.Venue,
            concert.ShowDate,
            concert.Photo
        );

        return new ConcertDto
        (
            concert.Guid,
            basedto,
            ToFormattedDateString(concert.ShowDate)
        );
    }

    public static Concert ToEntity(this ConcertCreateDto concertCreateDto)
    {
        ArgumentNullException.ThrowIfNull(concertCreateDto);

        var basedto = new ConcertBaseDto
        (
            concertCreateDto.basedto.Artist,
            concertCreateDto.basedto.Venue,
            concertCreateDto.basedto.ShowDate,
            concertCreateDto.basedto.Photo
        );

        return new Concert
        {
            Artist = basedto.Artist,
            Venue = basedto.Venue,
            ShowDate = basedto.ShowDate,
            Photo = basedto.Photo,
            UserId = string.Empty,
        };
    }

    public static void UpdateEntity(this Concert concert, ConcertUpdateDto concertUpdateDto)
    {
        ArgumentNullException.ThrowIfNull(concert);

        ArgumentNullException.ThrowIfNull(concertUpdateDto);

        concert.Artist = concertUpdateDto.basedto.Artist;
        concert.Venue = concertUpdateDto.basedto.Venue;
        concert.ShowDate = concertUpdateDto.basedto.ShowDate;
        concert.Photo = concertUpdateDto.basedto.Photo;
    }
}
