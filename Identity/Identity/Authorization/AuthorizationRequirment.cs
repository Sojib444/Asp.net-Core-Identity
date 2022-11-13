using Microsoft.AspNetCore.Authorization;

namespace Identity.Authorization
{
    public class AuthorizationRequirment:IAuthorizationRequirement
    {
        public AuthorizationRequirment( int ProbationPriod)
        {
            this.ProbationPriod = ProbationPriod;
        }

        public int ProbationPriod { get; }
    }

    public class AuthorizationRequirmentHandeler : AuthorizationHandler<AuthorizationRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        AuthorizationRequirment requirement)
        {
            if(!context.User.HasClaim(e => e.Type == "EmpolyeDate"))
            {
                return Task.CompletedTask;
            }

            var startDate = DateTime.Parse(context.User.FindFirst(e => e.Type == "EmpolyeDate").Value);
            var result = DateTime.Now - startDate;
            if(result.TotalDays>30*requirement.ProbationPriod)
            {
               context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
