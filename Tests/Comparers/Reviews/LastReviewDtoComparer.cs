using Application.DTOs.Reviews;

namespace Tests.Comparers.Reviews
{
    public class LastReviewDtoComparer : IComparer<LastReviewDto>
    {
        public int Compare(LastReviewDto? x, LastReviewDto? y)
        {
            if (!x!.ReviewId.Equals(y!.ReviewId, StringComparison.OrdinalIgnoreCase))
            {
                return StringComparer.OrdinalIgnoreCase.Compare(x!.ReviewId, y!.ReviewId);
            }

            if (x.Heading != y.Heading)
            {
                return x.Heading.CompareTo(y.Heading);
            }

            if (x.Content != y.Content)
            {
                return x.Content.CompareTo(y.Content);
            }

            if (x.CreatedAt != y.CreatedAt)
            {
                return x.CreatedAt.CompareTo(y.CreatedAt);
            }

            if (x.IsMine != y.IsMine)
            {
                return x.IsMine.CompareTo(y.IsMine);
            }

            return 0;
        }
    }
}