namespace Risk_n_Reward.Core;

public interface IGameEngine<TInput, TResult>
{
    TResult Evaluate(TInput input);
}