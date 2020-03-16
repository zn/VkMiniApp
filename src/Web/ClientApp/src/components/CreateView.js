import React, {Component} from 'react'
import {View, Panel, PanelHeader} from '@vkontakte/vkui'

class CreateView extends Component{
    
    render(){
        const {id} = this.props
        return (
            <View id={id} activePanel="create">
                <Panel id="create">
                    <PanelHeader>Создать объявление</PanelHeader>
                </Panel>
            </View>
        )
    }
}

export default CreateView