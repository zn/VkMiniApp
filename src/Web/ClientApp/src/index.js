import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import bridge from '@vkontakte/vk-bridge'

const rootElement = document.getElementById('root');

bridge.send("VKWebAppInit", {});
ReactDOM.render(<App />, rootElement);

