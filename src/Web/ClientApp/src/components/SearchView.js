import React, {Component} from 'react'
import {View, Panel, PanelHeader} from '@vkontakte/vkui'

class SearchView extends Component{
    
    render(){
        const {id} = this.props
        return (
            <View id={id} activePanel="search">
                <Panel id="search">
                    <PanelHeader>Поиск</PanelHeader>
                </Panel>
            </View>
        )
    }
}

export default SearchView