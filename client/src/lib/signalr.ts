import { HubConnectionBuilder } from '@microsoft/signalr';

const createConnection = (endpoint: string) => new HubConnectionBuilder()
    .withUrl(`${API_URL}${endpoint}`)
    .withAutomaticReconnect()
    .build();

export { createConnection };