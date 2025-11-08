using MyCourse.Models.ValueObjects;

public class CourseDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ImagePath { get; set; }
    public float Rating { get; set; }
    public Money FullPrice { get; set; }
    public Money CurrentPrice { get; set; }
    public string Description { get; set; }
    //TODO: Aggiungere Lezioni e durata totale corso
}