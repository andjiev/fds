import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

class FdsHubConnection {
  private static instance: HubConnection;

  private constructor() { }

  public static getInstance(endpoint: string): HubConnection {
    if (!FdsHubConnection.instance) {
      FdsHubConnection.instance = new HubConnectionBuilder()
        .withUrl(`http://localhost:6401/${endpoint}`)
        .withAutomaticReconnect()
        .build();
    }

    return FdsHubConnection.instance;
  }
}

export { FdsHubConnection };