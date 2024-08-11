using EduMaster.Domain.Entity;
using EduMaster.Domain.Repository;

namespace EduMaster.Infra.Repository.Memory;

public class QuizRepositoryMemory : IQuizRepository
{
    private IList<Quiz> _quiz;

    public QuizRepositoryMemory()
    {
        _quiz = [];
    }
   
    public Task<ICollection<Quiz>> ListAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<ICollection<Quiz>>(_quiz);
    }

    public Task<Quiz?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_quiz.FirstOrDefault(quiz => quiz.Id == id));
    }

    public Task Save(Quiz entity, CancellationToken cancellationToken)
    {
        _quiz.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(Quiz entity, CancellationToken cancellationToken)
    {
        var index = _quiz.IndexOf(entity);

        if(index != -1)
        {
            _quiz[index] = entity;
        }

        return Task.CompletedTask;
    }

    public Task Delete(Quiz entity, CancellationToken cancellationToken)
    {
        _quiz.Remove(entity);
        return Task.CompletedTask;
    }
}
