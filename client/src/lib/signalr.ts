import { HubConnectionBuilder } from '@microsoft/signalr';

const createConnection = (endpoint: string) => new HubConnectionBuilder()
    .withUrl(`https://localhost:5001/${endpoint}`)
    .withAutomaticReconnect()
    .build();

export { createConnection };