import React from 'react';
import { View, Panel, Button, Div } from '@vkontakte/vkui';

function NewUserScreen(props){
    
    return ( 
        <View id={props.id} activePanel="main">
            <Panel id="main">
                <Div>Hello, {props.first_name}</Div>
                <Button onClick={props.onClick} size="xl">Поехали!</Button>
            </Panel>
        </View>
    )
}

export default NewUserScreen