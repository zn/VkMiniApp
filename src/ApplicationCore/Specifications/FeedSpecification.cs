using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class FeedSpecification : ISpecification<Post>
    {
        public FeedSpecification(int skip, int take)
        {
            Take = take;
            Skip = skip;
        }

        public Expression<Func<Post, bool>> Criteria => p => !p.IsDeleted; // maybe add filter by city here or when search

        public List<Expression<Func<Post, object>>> Includes { get; } = new List<Expression<Func<Post, object>>>
        {
            post => post.Author,
            post => post.Attachments
        };

        public Expression<Func<Post, object>> OrderBy => null;
        public Expression<Func<Post, object>> OrderByDescending => (p => p.PublishDate);

        public int Take { get; }

        public int Skip { get; }

        public bool IsPagingEnabled => true;

    }
}
