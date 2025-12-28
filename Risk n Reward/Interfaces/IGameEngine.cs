namespace Risk_n_Reward;

public interface IGameEngine<TInput, TResult>
{
    TResult Evaluate(TInput input);
}