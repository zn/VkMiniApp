import React, { Component } from 'react';
import { Group, Cell, Avatar } from '@vkontakte/vkui';

class Post extends Component {

    render() {
        const { post } = this.props;
        return (
            <Group>
                <Cell
                    size="l"
                    description={post.publishDate}
                    before={<Avatar src="https://pp.userapi.com/c841034/v841034569/3b8c1/pt3sOw_qhfg.jpg" />}
                    bottomContent={post.content}
                >
                    {post.authorVkId}
                </Cell>
            </Group>
        )
    }
}

export default Post;