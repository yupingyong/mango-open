using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Mango.Framework.Data;
namespace Mango.Module.Posts.Entity
{
    public partial class m_PostsAttention:EntityBase
    {
		
        /// <summary>
        /// 关注Id
        /// </summary>
        [Key]
        public int? AttentionId { get; set; }
		
        /// <summary>
        /// 帖子Id
        /// </summary>
        
        public int? PostsId { get; set; }
		
        /// <summary>
        /// 关注时间
        /// </summary>
        
        public DateTime? AttentionDate { get; set; }
		
        /// <summary>
        /// 用户Id
        /// </summary>
        
        public int? UserId { get; set; }
		
    }
}