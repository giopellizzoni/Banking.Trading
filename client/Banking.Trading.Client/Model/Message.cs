namespace Banking.Trading.Client.Model;

public record MessagePayload(Message message);

public record Message(Guid tradeId, string asset, Guid clientId, DateTime executedAt);
