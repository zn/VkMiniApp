import React, { Component } from 'react';
import { View, Panel, PanelHeader, Div } from '@vkontakte/vkui';
import Post from './Post';

class FeedView extends Component{
    
    state = {
        elements:[]
    }

    componentDidMount(){
        fetch('/posts')
            .then(result => result.json())
            .then(data=>{
                console.log(data);
                this.setState({
                    elements: data.slice()
                })
            })

    }

    render(){
        const {id} = this.props
        return (
            <View id={id} activePanel="feed">
                <Panel id="feed">
                    <PanelHeader>Лента</PanelHeader>
                    <Div style={{width:"10px"}}></Div>
                    {this.state.elements.map(elem => <Post key={elem.id} post={elem} />)}
                </Panel>
            </View>
        )
    }
}

export default FeedView